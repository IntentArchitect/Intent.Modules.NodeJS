import { L_SelfReferenceMultiple } from './l-self-reference-multiple.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToMany, JoinTable } from 'typeorm';

@Entity('l_self_reference_multiple')
export class L_SelfReferenceMultiple {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  selfRefMultipleAttr: string;

  @ManyToMany(() => L_SelfReferenceMultiple, { cascade: ['insert', 'update'] })
  @JoinTable()
  l_SelfReferenceMultiples: L_SelfReferenceMultiple[];

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}