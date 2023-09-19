import { TptDerivedClassForConcrete } from './tpt-derived-class-for-concrete.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpt_derived_class_for_concrete_associated')
export class TptDerivedClassForConcreteAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  derivedClassForConcreteId: string;

  @ManyToOne(() => TptDerivedClassForConcrete, { cascade: ['insert', 'update'], nullable: false })
  derivedClassForConcrete: TptDerivedClassForConcrete;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}