import { Texture } from './texture.entity';
import { Internode } from './internode.entity';
import { Inhabitant } from './inhabitant.entity';
import { Leaf } from './leaf.entity';
import { Tree } from './tree.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne, OneToOne, JoinColumn, ManyToMany, JoinTable, OneToMany } from 'typeorm';

@Entity('branch')
export class Branch {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  branchAttribute: string;

  @ManyToOne(() => Texture, { cascade: ['insert', 'update'], nullable: false })
  texture: Texture;

  @OneToOne(() => Internode, { cascade: true, eager: true, nullable: false })
  @JoinColumn()
  internode: Internode;

  @ManyToMany(() => Inhabitant, { cascade: ['insert', 'update'] })
  @JoinTable()
  inhabitants: Inhabitant[];

  @OneToMany(() => Leaf, (leaves) => leaves.branch, { cascade: true, eager: true })
  leaves: Leaf[];

  @ManyToOne(() => Tree, (tree) => tree.branches, { cascade: ['insert', 'update'], nullable: false, onDelete: 'CASCADE', orphanedRowAction: 'delete' })
  tree: Tree;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}