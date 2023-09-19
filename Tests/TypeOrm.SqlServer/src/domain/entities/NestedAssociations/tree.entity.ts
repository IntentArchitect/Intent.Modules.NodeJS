import { Branch } from './branch.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from 'typeorm';

@Entity('tree')
export class Tree {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  treeAttribute: string;

  @OneToMany(() => Branch, (branches) => branches.tree, { cascade: true, eager: true })
  branches: Branch[];

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}